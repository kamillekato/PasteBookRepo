
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
            location.reload();
        },
        error: function () {
            alert('Something went wrong');
        }
    })
}

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
            location.reload();
        },
        error: function () {
            alert('Something went wrong')
        }
    });
}