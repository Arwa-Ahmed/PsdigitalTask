$(function () {
    UpdateCartCookie();
 
    $(document).on('click', "#DeleteCartItem", function (e) {
        e.preventDefault();
        console.log("Dd")
        var id = $(this).attr("data-id");
        if (id != '') {
            UpdateCartItem("/Cart/RemoveCart", { "id": id })
        }
    })
    $(".item-quantity").on('change', function (e) {
        e.preventDefault();
        var id = $(this).closest("tr").attr("data-id");
        if (id != '') {
            UpdateCartItem("/Cart/UpdateQuantityInCart", { "id": id ,'quantity':$(this).val()})
        }
    })
})

function UpdateCartItem(url, parameters) {
    console.log("Dd")
    $.post(url, parameters,
        function (data) {
            if (data.ItemCount == 0) {
                $('#row-' + data.CartId).fadeOut('slow');
            } else {
                $('#item-count-' + data.CartId).val(data.ItemCount);
            }
            $('#Subtotal').text(data.CartTotal);
            $('.cart-count').text(data.CartCount);

        });
}

function UpdateCartCookie() {
    $.ajax({
        type: 'Get',
        contentType: 'application/json; charset=utf-8',
        url: '/Home/UpdateCartCookie',
        success: function (data) {
            $('.cart-count').html(data.CartCount)
        },
        error: function (data) {
            console.log(data);
        }
    });
}