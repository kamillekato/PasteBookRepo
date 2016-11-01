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
            },
            error: function () {
                alert('Something went wrong');
            }
        });
    }
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
 

$("#postButton").click(function () {
    CreatePost();
});

function ViewLikes(partialLikeUrl) {
    $("#modalContentRender").load(partialLikeUrl);
    $("#modalView").modal('show');
}
function SendComment(id, postOwnerID) {
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
            }
        });
    }
}
