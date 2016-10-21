function ReloadPartial() { 
    $("#divRenderAction").load(partialUrl);
    
}
 
function ReloadPartialFriend() {
    $("#dropdownFriend").load(partialFriendUrl);
}

 

CheckUserIfFriend();
function CheckUserIfFriend() {
    if (parseInt(posterID) == profileOwnerID) { 
    } else {
        var data = {
            userID: parseInt(posterID),
            friendID: profileOwnerID
        };
        $.ajax({
            url: checkUserIfFriendUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                if (data.Status == false) {
                    if (parseInt(posterID) != profileOwnerID) { 
                        $("#btnAddFriend").text("Add Friend");
                        $("#btnAddFriend").show();
                    }
                } else { 
                    relationship = data;
                    if (data.RequestFriend == "Y") {
                        $("#btnAddFriend").text("Friend");
                        $("#btnAddFriend").show();
                    } else { 
                        if (data.UserID == parseInt(posterID)) {
                            $("#btnAddFriend").text("Request");
                            $("#btnAddFriend").show();
                        } else {
                            $("#btnAddFriend").text("Accept Request");
                            $("#btnAddFriend").show();
                        }
                    }
                }
            }, error: function () {
                alert('Something went wrong')
            }
        });
    }
    

}


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
            CheckUserIfFriend();
        },
        error: function () {
            alert('Something went wrong')
        }
    });
}

function CountFriendRequest() {
    var data = {
        userID: parseInt(posterID)
    }
    $.ajax({
        url: countFriendRequestUrl,
        data: data,
        type: 'GET',
        success: function (result) {
            $("#bdgeFriend").text(result);
        } 
    });
}

AutoRefresh();
function AutoRefresh() { 
    setInterval(function () { 
        CountFriendRequest();
    }, 3000);
}


function AcceptFriendRequest() { 
    var data = {
        ID: parseInt(relationship.ID),
        USER_ID: parseInt(posterID),
        FRIEND_ID: profileOwnerID
    };

    $.ajax({
        url: acceptFriendRequestUrl,
        data: data,
        type: 'GET',
        success: function () {
            CheckUserIfFriend();
        }, error: function () {
            alert('Somethign went wrong')
        }
    });
}
$("#btnAddFriend").on('click', function () { 
    if (relationship == null ) {
        AddFriend();
    } else { 
        if (relationship.RequestFriend == "N") {
            if (relationship.UserID != parseInt(posterID)) {
                AcceptFriendRequest();
            } 
        }
    }
});


function ViewLikes(partialLikeUrl) { 
    $("#modalContentRender").load(partialLikeUrl);
    $("#modalView").modal('show');
}
function SendComment(id) {
    var textAreaID = "#" + id.toString(); 
    var data = {
        content: $(textAreaID).val(),
        userID: parseInt(posterID),
        postID: id
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
        },
        error: function () {
            alert('Something went wrong');
        }
    })
}

 

function LikeUnlikePost(id) {
    var data = {
        postID : id,
        userID : posterID
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