function ReloadPartialFriend() {
    
    $("#dropdownFriend").load(partialFriendUrl);
}
 
function ReloadPartialNotification() {
     
    $("#dropdownNotification").load(partialNotificationUrl);
}

function LoadImage() {
    var data = {
        userID: parseInt(posterID)
    };
    $.ajax({
        url: getUserImageUrl,
        data:data,
        type: 'GET',
        success: function (data) {
            if (data.imageString != null) { 
                $('#imgNavBar').attr('src', 'data:image/png;base64,' + data.imageString);
            }
        } 
    });
}
LoadImage();



$("#searchBtn").click(function () {
    $(location).attr('href', '/Pastebook/Pastebook/SearchUser?searchText=' + $("#searchTextBox").val() );
});

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
                $("#bdgeFriend").show();
                $("#bdgeFriend").text(result.countFriendRequest.toString());
            } else {
                $("#bdgeFriend").hide();
            }
            if (result.countNotification > 0) {
                $("#bdgeNotification").show();
                $("#bdgeNotification").text(result.countNotification.toString());
            } else {
                $("#bdgeNotification").hide();
            }
        }
    });
}


CountNotification();
function TimerNotification() {
    setInterval(function () {
        CountNotification();
    }, 5000);
}
TimerNotification();


function CancelFriendByNotification(userID, friendID) {
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


$("#dropDownFriendRequest").click(function () {
    ReloadPartialNotification();
});
$("#dropDownNotification").click(function () {
    ReloadPartialNotification();
});

function AcceptFriendByNotification(userID, friendID) {
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
