$(document).ready(function () {
    var URLOF = window.location.pathname;
    if (~URLOF.indexOf("/Default.aspx"))
    {
        $('#DB').addClass('active');
    }
    else if (~URLOF.indexOf("/Catagories.aspx") || ~URLOF.indexOf("/CategoryView.aspx"))
    {
        $('#Ms').addClass('active');
        $('#CT').addClass('active');
    }
    else if (~URLOF.indexOf("/SuperCatAdd.aspx") || ~URLOF.indexOf("/SuperCatView.aspx")) {
        $('#SC').addClass('active');
        $('#Ms').addClass('active');
    }
    else if (~URLOF.indexOf("/Units.aspx") || ~URLOF.indexOf("/UnitsAdd.aspx")) {
        $('#UN').addClass('active');
        $('#Ms').addClass('active');
    }
    else if (~URLOF.indexOf("/ProjectView.aspx") || ~URLOF.indexOf("/ProjectAdd.aspx")) {
        $('#PJ').addClass('active');
    }
    else if (~URLOF.indexOf("/PricingView.aspx") || ~URLOF.indexOf("/PricingAdd.aspx")) {
        $('#PRI').addClass('active');
        $('#Ms').addClass('active');
    }
    else if (~URLOF.indexOf("/ViewSubItems.aspx") || ~URLOF.indexOf("/AddSubItems.aspx")) {
        $('#SI').addClass('active');
        $('#Ms').addClass('active');
    }
    else if (~URLOF.indexOf("/ViewEstimates.aspx") || ~URLOF.indexOf("/ViewEstimates.aspx")) {
        $('#EST').addClass('active');
    }



    $(document).bind('cbox_open', function () {
        position = $("html,body").scrollTop();
        $('html').css({
            overflow: 'hidden'
        });
    }).bind('cbox_closed', function () {
        $('html').css({
            overflow: 'auto'
        });
    })
})




       