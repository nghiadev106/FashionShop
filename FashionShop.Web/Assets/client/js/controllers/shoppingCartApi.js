﻿var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#frmPayment').validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Yêu cầu nhập tên",
                address: "Yêu cầu nhập địa chỉ",
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                },
                phone: {
                    required: "Số điện thoại được yêu cầu",
                    number: "Số điện thoại phải là số."
                }
            }
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {

                var amount = quantity * price;

                $('#amount_' + productid).text(numeral(amount).format('0,0'));

                $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));

                cart.updateAll();
            }
            else {
                $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                $('#amount_' + productid).text(0);
            }
        });
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });
        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });
        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }

        });

    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },

    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "Thanh toán tiền mặt",
            PaymentStatus: "Chờ xác nhận",
            Status: false
        }
        $.ajax({
            url: '/api/orders/create-order',
            type: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(order),
            success: function (response) {
                if (response) {
                    console.log('create order ok');
                    $('#divCheckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('#cartContent').html('<div class="alert alert-success alert-dismissible"><strong>Thành công!</strong> Bạn đã đặt hàng thành công!. </div>');
                    }, 1000);

                }
            }
        });
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },
    deleteAll: function () {
        $.ajax({
            url: '/api/carts/delete-all',
            type: 'DELETE',
            dataType: 'json',
            success: function (response) {
                if (response) {
                    cart.loadData();
                }
                    
            }
        });
    },
    updateAll: function () {
        var item = {
            ProductId: $('.txtQuantity').data('id'),
            Quantity: parseInt($('.txtQuantity').val())
        }
        $.ajax({
            url: '/api/carts/update-cart',
            type: 'PUT',
            data: JSON.stringify(item),
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                if (response) {
                    cart.loadData();
                    console.log('Update ok');
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: '/api/carts/delete-item/' + productId,
            data: {
                productId: productId
            },
            type: 'DELETE',
            dataType: 'json',
            success: function (response) {
                if (response) {
                    cart.loadData();
                }
            }
        });
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    });

                    $('#cartBody').html(html);

                    if (html == '') {
                        $('#cartContent').html(' <div class="alert alert-danger"><strong>Không có sản phẩm nào trong giỏ hàng!</strong> Tiếp tục mua hàng</div></br><button class="btn btn-success" id="btnContinue">Tiếp tục mua hàng</button>');

                    }
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        })
    }
}
cart.init();