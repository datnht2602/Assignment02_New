// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

showInPopup = (url,title)=>{
    $.ajax({
        type: "Get",
        url: url,
        success: function(res){
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $("#form-modal").modal('show')
        }
    })
}
jQueryAjaxPost = form =>{
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function(res){
                if(res.isValid){
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal .modal-title').html(res.html);
                    $("#form-modal").modal('hide');
                    toastr.success("Successful");
                    setTimeout(function(){// wait for 5 secs(2)
                        window.location.href = "/pizzas" // then reload the page.(3)
                   }, 1000); 
                }
                else
                $('#form-modal.modal-body').html(res.html);
            }
        })
    } catch (error) {
        console.log(error);
    }
    return false;
}
jQueryAjaxDelete = form =>{
    if(confirm("Are you sure delete this record")){
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function(res){
                        $('#form-modal .modal-body').html(res.html);
                        $('#form-modal .modal-title').html(res.html);
                        $("#form-modal").modal('hide');
                        toastr.success("Deleted success");
                        setTimeout(function(){// wait for 5 secs(2)
                            window.location.href = "/pizzas" // then reload the page.(3)
                       }, 1000); 
                    },errror: function(err){
                        console.log(err)
                    } 
            })
        } catch (error) {
            console.log(error);
        }
    }
   return false;
}