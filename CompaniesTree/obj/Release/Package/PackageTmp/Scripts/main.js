function openAdd(parentId) {
    $('#parentId').val(parentId);
    $('#overlay').show();
    $('#title').focus();
}

function openUpdate(nodeId, title, earnings) {
    $('#overlayUpdate').show();
    $('#nodeIdToUpdate').val(nodeId);
    $('#titleToUpdate').val(title).show();
    $('#earningsToUpdate').val(earnings).show();
}

function checkAndAdd() {
    $('#title').val() ? $('#AddForm').submit() :  $('#title').focus();
}

function checkAndUpdate() {
    $('#nodeIdToUpdate').val() && $('#titleToUpdate').val() && $('#earningsToUpdate').val() ? $('#UpdateForm').submit() : $('#titleToUpdate').focus();
}

function expand(elem) {
    var li = $(elem).parents('li')[0];
    var ul = $(li).children('ul')[0];
    $(ul).css('display') == 'none' ? $(ul).show() : $(ul).hide();
}