
 

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
            location.reload();
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
    var errorComment = "#errorComment"
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
                location.reload();
            },
            error: function () {
                alert('Something went wrong')
            }
        });
    }
}