$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');

        var quantity = 1;
        var quantityText = $("#quantity_value").text();
        if (quantityText != '') {
            quantity = parseInt(quantityText);
        }
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'Post',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });
    });
    $('body').on('click', '.btnDeleteItem', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có muốn xóa sản phẩm này khỏi giỏ hàng không ?');
        if (conf === true) {
            $.ajax({
                url: 'shoppingcart/deleteItem',
                type: 'Post',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        // alert(rs.msg);
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadCart();
                    }
                }
            });
        }

    });
    $('body').on('click', '.btnDeleteAllItem', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa tất cả sản phẩm trong giỏ hàng chứ ಠ_ಠ');
        
        if (conf === true) {
            DeleteAll();
            $('#checkout_items').html(0);
            LoadCart();
        }
    });
    $('body').on('click', '.btnUpdateItem', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $('#Quantity_' + id).val();
        UpdateQuantityOfItem(id,quantity);
    });

});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'Get',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}



// hàm load lại partial giỏ hàng mỗi khi cập nhật đơn hàng
function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'Get',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}

// hàm xóa hết sản phẩm trong giỏ hàng
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAllItem',
        type: 'Post',
        success: function (rs) {
            if (rs.Success) {
                // xóa hết những bản ghi trong cart -> chỉ cần gọi hàm load lại cart là được
                LoadCart();
            }
        }
    });
}

function UpdateQuantityOfItem( id,  quantity) {
    $.ajax({
        url: '/shoppingcart/UpdateQuantityOfItem',
        type: 'Post',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}