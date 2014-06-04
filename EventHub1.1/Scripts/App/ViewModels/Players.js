define(["ViewModels/Player"], function (Player) {
    var players = function (arrayOfPlayers) {

        var self = this;

        self.listOfPlayers = ko.observableArray();

        // Constructor
        for (var i = 0; i < arrayOfPlayers.length; i++) {
            self.listOfPlayers.push(new Player(arrayOfPlayers[i]));
        }

        self.toggleActivePlayer = function (clickedPlayerInListOfPlayers) {
            $.ajax({
                url: "http://localhost:29196/user/ToggleActiveById/" + clickedPlayerInListOfPlayers.userId(),
                context: document.body,
                type: 'POST',
                statusCode: {
                    200: function () {
                        self.listOfPlayers.remove(clickedPlayerInListOfPlayers);
                    }
                }
            })
        }

        self.toggleAdminPrivileges = function (clickedPlayerInListOfPlayers) {
            $.ajax({
                url: "http://localhost:29196/user/ToggleAdminById/" + clickedPlayerInListOfPlayers.userId(),
                context: document.body,
                type: 'POST',
                statusCode: {
                    200: function () {
                        clickedPlayerInListOfPlayers.isAdmin(!clickedPlayerInListOfPlayers.isAdmin());
                    }
                }
            })
        }

        self.showInactiveUsers = function (clickedPlayerInListOfPlayers) {
            $.blockUI({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });

            if (sessionStorage.getItem("viewingActiveUsers") == "true") {
                sessionStorage.setItem("viewingActiveUsers", false)
                self.listOfPlayers.removeAll();
                //$('#toggleBetweenUsersButton').html("See Active")
                $.ajax({
                    dataType: "json",
                    url: "http://localhost:29196/user/GetInactive",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function (arrayOfInactiveUsers) {
                            for (var i = 0; i < arrayOfInactiveUsers.length; i++) {
                                self.listOfPlayers.push(new Player(arrayOfInactiveUsers[i]));
                            }
                        }
                    }
                })
            }
            else if (sessionStorage.getItem("viewingActiveUsers") == "false") {
                sessionStorage.setItem("viewingActiveUsers", true)
                self.listOfPlayers.removeAll();
                //$('#toggleBetweenUsersButton').html("See Inactive")
                $.ajax({
                    dataType: "json",
                    url: "http://localhost:29196/user/",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function (arrayOfActiveUsers) {
                            for (var i = 0; i < arrayOfActiveUsers.length; i++) {
                                self.listOfPlayers.push(new Player(arrayOfActiveUsers[i]));
                            }
                        }
                    }
                })
            }

            setTimeout($.unblockUI, 0);
        }
    }

    return players;
});