$(document).ready(function () {
    var $theForm = $("#theForm");
    var $buyButton = $("#buyButton");
    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    //$theForm.hide();

    $buyButton.on("click", function () {
        console.log("Buying Item");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());
    });

    $loginToggle.on("click", function () {
        $popupForm.toggle(1000);
    })




});