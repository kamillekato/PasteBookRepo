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

function LikeUnlikePost(postID, postOwnerID) {
    var data = {
        postID: postID,
        userID: posterID,
        postOwnerID: postOwnerID
    };
    $.ajax({
        url: likeUnlikeUrl,
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
    var data = {
        content: $(textAreaID).val(),
        userID: parseInt(posterID),
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
