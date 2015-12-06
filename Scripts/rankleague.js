$(document).ready(function () {

    var leagueRows = $(".league-row");

    for (i = 0; i < leagueRows.length; i++) {
        $($(leagueRows[i]).children(".rank")[0]).html(String(i + 1));
    }

    $(".league-row:odd").css({ "background-color": "#eee" });

});

