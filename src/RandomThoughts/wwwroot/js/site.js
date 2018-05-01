﻿// Write your JavaScript code.

var apiHost;

$(document).ready(function(){
    apiHost = $('#app-data').attr('data-apiHost');
    console.log(apiHost);
    $(".thought-view-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        getThought(thoughtId);        
    });
    $(".thoughtHole-view-btn").click(function () {
        let thoughtHoleId = $(this).closest(".thoughtHole-inner-container").data("id");
        getThoughtHole(thoughtHoleId);
    });
    $(".thought-edit-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        displayThoughtEditModal(thoughtId);        
    });
    $(".thoughtHole-edit-btn").click(function () {
        let thoughtHoleId = $(this).closest(".thoughtHole-inner-container").data("id");
        displayThoughtHoleEditModal(thoughtHoleId);
    });
    $('#add-new-thought').click(function () {
        $('#save-thought-btn').text("Save");
        $('#thought-edit-modal').modal('show');
    });
    $('#save-thought-btn').click(function(e){
        e.preventDefault();
        saveNewThought();
    });

    $('#save-thoughtHole-btn').click(function(e){
        e.preventDefault();
        saveNewThoughtHole();
    });

    $('#thought-edit-modal').on('hidden.bs.modal', function () {
        cleanModal();
    });

    $('#add-new-thoughtHole').click(function(){
        $('#thoughtHole-edit-modal').modal('show');
    });

});

/** Thought  functions Start */

/**
 * Get the Thought data from the server.
 * @param  {number} thoughtId The Id of the selected Thought
 */
function getThought(thoughtId){
    $.get(apiHost + 'thoughts/get/' + thoughtId, function (data) {
        displayThoughtDetails(data);
    });
}
/**
 * TODO: Add the dates to the modal.
 */
function displayThoughtDetails(data) {
    var v = data.visibility == 0 ? " (Private)" : " (Public)";
    $('#thought-display-modal .modal-title').html(data.title + v);
    $('#thought-display-modal .modal-body > p').html(data.body);
    $('#thought-display-modal').modal('show');
}

function saveNewThought(){
    let inputsSelector = $('#thought-edit-modal input, #thought-edit-modal textarea, #thought-edit-modal select, #thought-edit-modal select');
    var data = inputsSelector.serializeArray();
    let hasError = false;
    for(key in data){
        if(data[key].value==""&&data[key].name!="Id"&&data[key].name!="ThoughtHoleId"){
            var outerSelector = $('input[name="'+data[key].name+'"]').closest('.form-group');
            if(outerSelector.length == 0)   
                outerSelector = $('textarea[name="'+data[key].name+'"]').closest('.form-group');
            outerSelector.addClass('has-error');
            console.log(data[key]);
            hasError = true;
        }
    }
    if(hasError){
        toastr.error('There are some error in the form, please fix them', 'Opps');
        return false;
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
        insertNewThought(res);
        cleanModal();

    }).error(function (jqXHR, textStatus, errorThrown) {
        toastr.error('There is an error in the server, please try again :(', 'Opps');
    });
}

function cleanModal(){
    $('#thought-edit-modal .form-group').removeClass('has-error');
    $('#thought-edit-modal input, #thought-edit-modal textarea').val('');
    $('#thought-edit-modal').modal('hide');
}

function displayThoughtEditModal(thoughtId){
    var thoughtCntSel = $('.thought-inner-container[data-id="'+thoughtId+'"]');
    let title = thoughtCntSel.find('.thought-title').text();
    let body = thoughtCntSel.find('.thought-body').text();
    let mood = thoughtCntSel.attr('data-mood');
    let visibility = thoughtCntSel.attr('data-visibility');

    console.log(title);
    console.log(body);
    console.log(visibility);

    $('#thought-edit-modal #thought-id').val(thoughtId);

    $('#thought-edit-modal #thought-title').val(title);
    $('#thought-edit-modal #thought-body').val(body);
    $('#thought-edit-modal #thought-mood').val(mood);
    $('#thought-edit-modal #thought-visibility').val(visibility);

    $('#save-thought-btn').text("Edit").unbind('click').
    click(function(){
        saveThoughtChanges(thoughtId);
    });
    $('#thought-edit-modal').modal('show');

}

function saveThoughtChanges(thoughtId){
    let inputsSelector = $('#thought-edit-modal input, #thought-edit-modal textarea, #thought-edit-modal select, #thought-edit-modal select');
    var data = inputsSelector.serializeArray();
    let hasError = false;
    for(key in data){
        if(data[key].value==""){
            var outerSelector = $('input[name="'+data[key].name+'"]').closest('.form-group');

            if(outerSelector.length == 0)   
                 outerSelector = $('textarea[name="'+data[key].name+'"]').closest('.form-group');

            outerSelector.addClass('has-error');
            console.log(data[key]);
            hasError = true;
        }
    }
    if(hasError){
        toastr.error('There are some error in the form, please fix them', 'Opps');
        return false;
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
        $(".thought-edit-btn").unbind('click').click(function() {
            let thoughtId = $(this).closest(".thought-inner-container").data("id");
            displayThoughtEditModal(thoughtId);        
        });
        cleanModal();
        editThoughtCard(res);
    }).error(function (jqXHR, textStatus, errorThrown) {
        toastr.error('There is an error in the server, please try again :(', 'Opps');
    });
}

function editThoughtCard(thought){
    $('.thought-inner-container[data-id="'+thought.id + '"] .thought-title').text(thought.title);
    $('.thought-inner-container[data-id="'+thought.id + '"] .thought-body').text(thought.body);
    $('.thought-inner-container[data-id="' + thought.id + '"]').attr('data-mood', thought.mood);
    $('.thought-inner-container[data-id="' + thought.id + '"]').attr('data-visibility', thought.visibility);
}

function insertNewThought(thought) {
    let element = '<div data-mood="' + thought.mood + '" data-visibility="' + thought.visibility + '" data-id="' + thought.id +'" class="thought-inner-container col-sm-3">\
    <div class="panel panel-default">\
        <div class="panel-heading">\
           <span class="thought-title">'+ thought.title + '</span>\
            <span class="thought-action-container pull-right">' +
                '<button class="thought-view-btn">' +              
                    '<span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>\
                </button>\
                <button class="thought-edit-btn">'+
                    '<span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>\
                </button>\
            </span>\
        </div>\
        <div class="panel-body">\
        <span class="thought-body">'+thought.body+'</span>'+
        '</div>'+
    '</div>'+
'</div>';
    $('#thoughts-container').prepend(element);

    $(".thought-view-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        getThought(thoughtId);        
    });
    $(".thought-edit-btn").click(function() {
        let thoughtId = $(this).closest(".thought-inner-container").data("id");
        displayThoughtEditModal(thoughtId);        
    });
}

/** Thought  functions End */


/** Thought Holes functions Start */

/**
 * Get the ThoughtHole data from the server.
 * @param  {number} thoughtHoleId The Id of the selected Thought
 */
function getThoughtHole(thoughtHoleId) {

    $.get(apiHost + 'ThoughtHoles/get/' + thoughtHoleId, function (data) {
        displayThoughtHoleDetails(data);
    });
}
/**
 * TODO: Add the dates to the modal.
 */
function displayThoughtHoleDetails(data) {
    var v = data.visibility == 0 ? " (Private)" : " (Public)";
    $('#thoughtHole-display-modal .modal-title').html(data.name + v);
    $('#thoughtHole-display-modal .modal-body > p').html(data.description);
    $('#thoughtHole-display-modal').modal('show');
}

function saveNewThoughtHole(){
    let inputsSelector = $('#thoughtHole-edit-modal input, #thoughtHole-edit-modal textarea, #thoughtHole-edit-modal select');
    var data = inputsSelector.serializeArray();
    let hasError = false;
    for(key in data){
        if(data[key].value==""&&data[key].name!="Id"){
            var outerSelector = $('input[name="'+data[key].name+'"]').closest('.form-group');
            if(outerSelector.length == 0)   
                outerSelector = $('textarea[name="'+data[key].name+'"]').closest('.form-group');
            outerSelector.addClass('has-error');
            console.log(data[key]);
            hasError = true;
        }
    }
    if(hasError){
        toastr.error('There are some error in the form, please fix them', 'Opps');
        return false;
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
        url: apiHost + 'ThoughtHoles/post',
        contentType: "application/json"

    }).done(function (res) {
        console.log('res', res);      
        toastr.success("The Hole have been created!!!");     
        insertNewThoughtHole(res);
        cleanHoleModal();

    }).error(function (jqXHR, textStatus, errorThrown) {
        toastr.error('There is an error in the server, please try again :(', 'Opps');
    });
}

function insertNewThoughtHole(hole) {
    let element = '<div data-likes="' + hole.likes + '" data-visibility="' + hole.visibility + '" data-views="' + hole.views + '"  data-id="' + hole.Id +'" class="thoughtHole-inner-container col-sm-3">\
    <div class="panel panel-default">\
        <div class="panel-heading">\
           <span class="thought-title">'+ hole.name + '</span>'+
            '<span class="thought-action-container pull-right">' +    
                '<button class="thought-view-btn">' +                 
                    '<span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>\
                </button>\
                <button class="thought-edit-btn">\
                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>\
                </button>\
            </span>\
        </div>\
        <div class="panel-body">\
        <span class="thought-body">'+hole.description+'</span>\
        </div>\
    </div>\
</div>';

//<a href="@Url.Action("HoleThoughts", new {holeId = thoughtHole.Id})">
    $('#thoughtHoles-container').prepend(element);

    $(".thoughtHole-view-btn").click(function() {
        let thoughtId = $(this).closest(".thoughtHole-inner-container").data("id");
        getThoughtHole(thoughtId);
    });
    $(".thoughtHole-edit-btn").click(function() {
        let thoughtId = $(this).closest(".thoughtHole-inner-container").data("id");
        displayThoughtHoleEditModal(thoughtId);        
    });
}

function cleanHoleModal(){
    $('#thoughtHole-edit-modal .form-group').removeClass('has-error');
    $('#thoughtHole-edit-modal input, #thoughtHole-edit-modal textarea').val('');
    $('#thoughtHole-edit-modal').modal('hide');
}

function displayThoughtHoleEditModal(thoughtHoleId) {
    var thoughtHoleCntSel = $('.thoughtHole-inner-container[data-id="'+thoughtHoleId+'"]');
    let name = thoughtHoleCntSel.find('.thoughtHole-name').text();
    let description = thoughtHoleCntSel.find('.thoughtHole-description').text();
    let visibility = thoughtHoleCntSel.attr('data-visibility');

    console.log(name);
    console.log(description);
    console.log(visibility);

    $('#thoughtHole-edit-modal #thoughtHole-name').val(name);
    $('#thoughtHole-edit-modal #thoughtHole-description').val(description);
    $('#thoughtHole-edit-modal #thoughtHole-visibility').val(visibility);

    $('save-ThoughtHole-btn').text('Edit').unbind('click').
        click(function () {
            saveThoughtHoleChanges(thoughtHoleId);
        });
    $('#thoughtHole-edit-modal').modal('show');
}

function saveThoughtHoleChanges(thoughtHoleId) {
    let inputsSelector = $('#thoughtHole-edit-modal input, #thoughtHole-edit-modal textarea, #thoughtHole-edit-modal select');
    var data = inputsSelector.serializeArray();
    let hasError = false;
    for (key in data) {
        if (data[key].value == "") {
            var outerSelector = $('input[name="' + data[key].name + '"]').closest('.form-group');

            if (outerSelector.length == 0)
                outerSelector = $('textarea[name="' + data[key].name + '"]').closest('.form-group');

            outerSelector.addClass('has-error');
            console.log(data[key]);
            hasError = true;
        }
    }
    if (hasError) {
        toastr.error('There are some error in the form, please fix them', 'Opps');
        return false;
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
        url: apiHost + 'ThoughtHoles/put/' + thoughtId,
        contentType: "application/json"

    }).done(function (res) {
        console.log('res', res);
        toastr.success("The Thought have been modified!!!");
        $(".thoughtHole-edit-btn").unbind('click').click(function () {
            let thoughtId = $(this).closest(".thoughtHole-inner-container").data("id");
            displayThoughtEditModal(thoughtId);
        });
        cleanModal();
        editThoughtHoleCard(res);
    }).error(function (jqXHR, textStatus, errorThrown) {
        toastr.error('There is an error in the server, please try again :(', 'Opps');
    });
}

function editThoughtHoleCard(thoughtHole) {
    $('.thoughtHole-inner-container[data-id="' + thoughtHole.id + '"] .thoughtHole-name').text(thoughtHole.name);
    $('.thoughtHole-inner-container[data-id="' + thoughtHole.id + '"] .thoughtHole-description').text(thoughtHole.description);
    $('.thoughtHole-inner-container[data-id="' + thoughtHole.id + '"]').attr('data-visibility', thoughtHole.visibility);
}

/** Thought Holes functions End */