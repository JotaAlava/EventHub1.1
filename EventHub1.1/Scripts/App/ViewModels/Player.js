define([], function () {
    var player = function (playerDTO) {
        var self = this;

        self.userId =   ko.observable(playerDTO.UserId);
        self.username = ko.observable(playerDTO.Username);
        self.name =     ko.observable(playerDTO.Name);
                            
        self.email =    ko.observable(playerDTO.EMail);
        self.isAdmin =  ko.observable(playerDTO.IsAdmin);
        self.active =   ko.observable(playerDTO.Active);

        //self.messages = "functionToGetMessages";
        //self.plusOnes = "functionToGetPlusOnes";
        //self.events = "functionToGetEvents";

        return self;
    }

    return player;
})