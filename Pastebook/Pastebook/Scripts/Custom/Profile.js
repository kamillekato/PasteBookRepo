function ReloadPartial() { 
    $("#divRenderAction").load(partialUrl);
    
} 
 
var timerNewsFeed;
function ClearTimer() {
    clearInterval(timerNewsFeed); 
}

function TimerNewsFeed() {
    ClearTimer()
     timerNewsFeed = setInterval(function () { 
        ReloadPartial();
    }, 60000);
}


TimerNewsFeed();
function AddFriend(ownerID) {
    var data = {  
        FRIEND_ID: ownerID
    };

    $.ajax({
        url: addFriendUrl,
        data: data,
        type: 'GET',
        success: function (data) { 
            location.reload();
        } 
    });
} 

function AcceptFriendRequest() { 
    var data = {  
        FRIEND_ID: profileOwnerID
    };

    $.ajax({
        url: acceptFriendRequestUrl,
        data: data,
        type: 'GET',
        success: function () { 
            location.reload();
        }, error: function () {
            alert('Something went wrong')
        }
    });
}
 

function CancelFriendRequest() {
    var data = {
        userID: userID,
        friendID: friendID
    };

    $.ajax({
        url: acceptInNotifUrl,
        data: data,
        type: 'GET',
        success: function () { 
            ReloadPartialFriend();
        }, error: function () {
            alert('Something went wrong')
        }
    });
}

function ViewLikes(partialLikeUrl) { 
    $("#modalContentRender").load(partialLikeUrl);
    $("#modalView").modal('show');
}
function SendComment(id,postOwnerID) {
    var textAreaID = "#" + id.toString();
    var errorComment = "#" + id.toString() + "Error";
    if ($(textAreaID).val() == "") {
        $(errorComment).text("You haven't type anything");
    } else if ($(textAreaID).val().length > 1000) {
        $(errorComment).text("The maximum allowable characters to comment is 1000");
    } else {
        var data = {
            content: $(textAreaID).val(),
            postID: id,
            postOwnerID: postOwnerID
        };

        $.ajax({
            url: sendCommentUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                $(textAreaID).val("");
                ReloadPartial();
            },
            error: function () {
                alert('Something went wrong')
            }
        });
    }
}

function CreatePost() {
    if ($("#textAreaPost").val() == "") {
        $("#postError").text("You haven't typed anything yet");
    } else if (($("#textAreaPost").val().length > 1000)) {
        $("#postError").text("The maximum allowable characters to post is 1000");
    } else { 
        var data = {
            content: $("#textAreaPost").val(),
            profile_Owner_ID: parseInt(profileOwnerID)
        };
        $.ajax({
            url: createPostUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                $("#textAreaPost").val("");
                ReloadPartial();
            }
        });
    }
}

function LikePost(postID, postOwnerID) {
    var data = {
        postID: postID, 
        postOwnerID: postOwnerID
    };
    $.ajax({
        url: likeUrl,
        data: data,
        type: 'GET',
        success: function (data) {
            ReloadPartial();
        },
        error: function () {
            alert('Something went wrong');
        }
    })
}
function UnlikePost(postID) {
    var data = {
        postID: postID 
    }

    $.ajax({
        url: unlikeUrl,
        data: data,
        type: 'GET',
        success: function (data) {
            ReloadPartial();
        },
        error: function () {
            alert('Something went wrong');
        }
    })

}

$("#postButton").click(function () { 
    CreatePost();
});

 

$("#btnSaveAboutMe").click(function () {
    SaveAboutMe();
})

$("#editAboutMe").click(function () {
    $("#aboutMeTextarea").val($("#aboutMeInfo").text());
});

$("#aboutMeTextarea").blur(function () {
    if ($("#aboutMeTextarea").val().length > 2000) {
        $("#aboutMeError").text("This field must be at most 2000 characters long");
    } else { 
        $("#aboutMeError").text("");
    }
});

 
function SaveAboutMe() {
    if ($("#aboutMeTextarea").val().length > 2000) {
        $("#aboutMeError").text("This field must be at most 2000 characters long");
    } else {
        var content = $("#aboutMeTextarea").val();
        var data = {
            aboutMeContent: content
        }
        $.ajax({
            url: saveAboutMeUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                if (data.Result == true) {
                    $("#modalAbout").modal('hide');
                    $("#aboutMeInfo").text(content);
                }
            }, error: function () {

            }
        });
    }
}


function ReadUrl(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#ImgProfile").attr('src', e.target.result);
            $("#imgNavBar").attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
} 
$("#inputUpload").change(function () {

    var ext = $('#inputUpload').val().split('.').pop().toLowerCase();
    var picsize = $('#inputUpload')[0].files[0].size;
    if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) { 
        $("#errorMessage").text("Invalid file upload. Profile picture must be image.");
        $("#modalError").modal("show");
    } else {
        if (picsize > 2097152) { 
            $("#errorMessage").text("Invalid file upload. Image size should not exceed 2mb.");
            $("#modalError").modal('show');
        } else {
            ReadUrl(this);
            var data = new FormData();
            var files = $("#inputUpload").get(0).files;
            if (files.length > 0) {
                data.append("UploadImage", files[0])
            }
            $.ajax({
                url: changePictureUrl,
                type: 'POST',
                processData: false,
                contentType: false,
                data: data,
                success: function (data) {
                    if (data.Result == true) {
                        ReloadPartial();
                    }
                }, error: function () {
                }
            });
        }
        
    }

});