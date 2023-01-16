$(function () {
    UpdateCartCookie();
    $('.addToCart').on('click', function () {
        var id = $(this).attr("data-id");
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: 'Cart/Add',
            data: { 'id': id },
            success: function (data) {
                $('.cart-count').text(data.CartCount);
            },
            error: function (data) {
                alert(data);
            }
        });
    });

    $("#DeleteCartItem").on('click', function (e) {
        e.preventDefault();
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
        url: 'Cart/UpdateCartCookie',
        success: function (data) {
            $.cookie(data.CookieKey, data.CookieValue);
            $('.cart-count').html(data.CartCount)
        },
        error: function (data) {
            alert(data);
        }
    });
}