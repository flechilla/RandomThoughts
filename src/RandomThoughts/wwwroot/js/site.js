// Write your JavaScript code.

var apiHost;

$(document).ready(function(){
    apiHost = $('#app-data').attr('data-apiHost');
    console.log(apiHost);
    $(".thought-view-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        getThought(thoughtId);        
    });
    $(".thought-edit-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        displayThoughtEditModal(thoughtId);        
    });
    $('#add-new-thought').click(function(){
        $('#thought-edit-modal').modal('show');
    });
    $('#save-thought-btn').click(saveNewThought);
});

/**
 * Get the Thought data from the server.
 * @param  {number} thoughtId The Id of the selected Thought
 */
function getThought(thoughtId){
    $.get(apiHost+'thoughts/get/'+thoughtId, function(data){
        displayThoughtDetails(data);
    });
}
/**
 * TODO: Add the dates to the modal.
 */
function displayThoughtDetails(data){
    $('#thought-display-modal .modal-title').html(data.title);
    $('#thought-display-modal .modal-body > p').html(data.body);
    $('#thought-display-modal').modal('show');

}

function saveNewThought(){
    let inputsSelector = $('#thought-edit-modal input');
    var data = inputsSelector.serializeArray();
    let hasError = false;
    for(key in data){
        if(data[key].value==""){
            var outerSelector = $('input#'+data[key].name.toLowerCase()).closest('.form-group');
            outerSelector.addClass('has-error');
            console.error(data[key]);
            hasError = true;
        }
    }
    let dataJson = {};
    let form = new FormData();
    data.map(function (x) {
        dataJson[x.name] = x.value;
    });

    console.log(data);
    $.ajax({
        type: "POST",
        data: JSON.stringify(dataJson),
        url: apiHost + 'Thoughts/post',
        contentType: "application/json"

    }).done(function (res) {
        console.log('res', res);      
        toastr.success("The Thought have been created!!!");     
    }).error(function (jqXHR, textStatus, errorThrown) {
        toastr.error('There is an error in the server, please try again :(', 'Opps');
    });
}

function displayThoughtEditModal(thoughtId){
    var thoughtCntSel = $('.thought-inner-container[data-id="'+thoughtId+'"]');
    let title = thoughtCntSel.find('.thought-title').text();
    let body = thoughtCntSel.find('.thought-body').text();

    $('#thought-edit-modal .modal-body').
    append('<input type="text" style="display: none;" name="Id" value="'+thoughtId+'">');

    $('#thought-edit-modal #thought-title').val(title);
    $('#thought-edit-modal #thought-body').val(body);

    $('#save-thought-btn').text("Edit").unbind('click').
    click(function(){
        saveThoughtChanges(thoughtId);
    });
    $('#thought-edit-modal').modal('show');

}

function saveThoughtChanges(thoughtId){
    let inputsSelector = $('#thought-edit-modal input');
    var data = inputsSelector.serializeArray();
    let hasError = false;
    for(key in data){
        if(data[key].value==""){
            var outerSelector = $('input#'+data[key].name.toLowerCase()).closest('.form-group');
            outerSelector.addClass('has-error');
            console.error(data[key]);
            hasError = true;
        }
    }
    let dataJson = {};
    let form = new FormData();
    data.map(function (x) {
        dataJson[x.name] = x.value;
    });

    console.log(data);
    $.ajax({
        type: "PUT",
        data: JSON.stringify(dataJson),
        url: apiHost + 'Thoughts/put/'+thoughtId,
        contentType: "application/json"

    }).done(function (res) {
        console.log('res', res);      
        toastr.success("The Thought have been modified!!!");     
    }).error(function (jqXHR, textStatus, errorThrown) {
        toastr.error('There is an error in the server, please try again :(', 'Opps');
    });
}