function ReloadPartial() { 
    $("#divRenderAction").load(partialUrl);
    
}
 
function ReloadPartialFriend() {
    $("#dropdownFriend").load(partialFriendUrl);
}

function ReloadPartialNotification() {
    $("#dropdownNotification").load(partialNotificationUrl);
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
                        if (profileOwnerID == parseInt(posterID)) {
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

function CountNotification() {
    var data = {
        userID: parseInt(posterID)
    }
    $.ajax({
        url: countNotificationUrl,
        data: data,
        type: 'GET',
        success: function (result) {
            if (result.countFriendRequest > 0) {
                $("#bdgeFriend").text(result.countFriendRequest);  
            }
            if (result.countNotification > 0) {
                $("#bdgeNotification").text(result.countNotification); 
            }
        } 
    });
}
 
CountNotification();
AutoRefresh();
function AutoRefresh() { 
    setInterval(function () { 
        CountNotification();
    }, 1000);
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
            alert('Something went wrong')
        }
    });
}
$("#btnAddFriend").on('click', function () { 
    if (relationship == null ) {
        AddFriend();
    } else { 
        if (relationship.RequestFriend == "N") {
            if (profileOwnerID != parseInt(posterID)) {
                AcceptFriendRequest();
            } 
        }
    }
});

function AcceptFriendByNotification(userID,friendID) {
    var data = { 
        userID: userID,
        friendID: friendID
    };

    $.ajax({
        url: acceptInNotifUrl,
        data: data,
        type: 'GET',
        success: function () {
            CheckUserIfFriend();
            ReloadPartialFriend();
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
            CheckUserIfFriend();
            ReloadPartialFriend();
        }, error: function () {
            alert('Something went wrong')
        }
    });
}

function CancelFriendByNotification(userID,friendID){
    var data = {
        userID: userID,
        friendID: friendID
    };

    $.ajax({
        url: cancelFriendUrl,
        data: data,
        type: 'GET',
        success: function () {
            CheckUserIfFriend();
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
        },
        error: function () {
            alert('Something went wrong');
        }
    })
} 
function LikeUnlikePost(postID,postOwnerID) {
    var data = {
        postID : postID,
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


$("#btnSaveAboutMe").click(function () {
    SaveAboutMe();
})
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
        success: function () {
            $("#modalAbout").modal('hide');
            $("#aboutMeInfo").text(content);
        }, error: function () {

        }
    });
}