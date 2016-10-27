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
function AddFriend() {
    var data = { 
        USER_ID: parseInt(posterID),
        FRIEND_ID: profileOwnerID
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
        USER_ID: parseInt(posterID),
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
    var data = {
        content: $(textAreaID).val(),
        userID: parseInt(posterID),
        postID: id,
        postOwnerID:postOwnerID
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

function CreatePost() {
    var data = {
        content: $("#textAreaPost").val(),
        poster_ID: parseInt(posterID),
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
    })
}

function LikePost(postID, postOwnerID) {
    var data = {
        postID: postID,
        userID: posterID,
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
        postID: postID,
        userID: posterID
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

HideShowLeftSide();
function HideShowLeftSide() {
    if (profileOwnerID == parseInt(posterID)) { 
        $("#editAboutMe").show();
        $("#divFriendShow").show(); 
    } else { 
        $("#editAboutMe").hide();
    }
}

$("#btnSaveAboutMe").click(function () {
    SaveAboutMe();
})

$("#editAboutMe").click(function () {
    $("#aboutMeTextarea").val($("#aboutMeInfo").text());
});
function SaveAboutMe() { 
    var content = $("#aboutMeTextarea").val();  
    var data = {
        userID : parseInt(posterID),
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

});