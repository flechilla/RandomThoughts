// Write your JavaScript code.

var apiHost;

$(document).ready(function(){
    apiHost = $('#app-data').attr('data-apiHost');
    console.log(apiHost);
    $(".thought-view-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        getThought(thoughtId);        
    });
});


function getThought(thoughtId){
    $.get(apiHost+'thoughts/get/'+thoughtId, function(data){
        displayThoughtDetails(data);
    });
}

function displayThoughtDetails(data){
    $('#thought-display-modal .modal-title').html(data.title);
    $('#thought-display-modal .modal-body > p').html(data.body);
    $('#thought-display-modal').modal('show');

}