function addProduct() {
    var response = getUploadedImage();
    response.then((d) => {
        const name = document.getElementById("product").value;
        const price = document.getElementById("price").value;
        const quantity = document.getElementById("quantity").value;

        let obj = {
            "id": 0,
            "name": name,
            "price": Number(price),
            "quantity": Number(quantity),
            "imageUrl":d
        };

        console.log("Object", obj);
        $.ajax({
            url: 'https://localhost:22955/p',
            method: "POST",
            data: JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response);
                location.href = "https://localhost:7224";
            }
        })
    });
}

function getUploadedImage() {
    var fileInput = document.getElementById("MyImage");
    if (fileInput.files.length == 0) {
        return "https://cdn.pixabay.com/photo/2022/05/23/12/49/product-7216106_640.png";
    }

    var file = fileInput.files[0];
    var formData = new FormData();
    formData.append("file", file);
    return $.ajax({
        url: 'https://localhost:22955/i',
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            return response;
        },
        error: function () {
            console.log("Request failed");
        }
    })
}

var products = [];

function CallGetAll() {
    $.ajax({
        url: 'https://localhost:22955/p',
        method: "GET",
        success: function (data) {
            products = data;
            let content = "";
            for (let i = 0; i < products.length; i++) {
                let item = `
                <div class='card m-3' style='width:18rem'>
                    <img class='card-img-top' style='height:350px' src='${data[i].imageUrl}' />
                    <div class='card-body'>
                    <h5 class='card-title'>${data[i].name}</h5>
                    <p class='card-text'>${data[i].price}</p>
                    <a class='btn btn-primary' onclick='SelectProduct(${data[i].id})'>Select Product</a>
                    </div>
                </div>
                `;
                content += item;
            }
            $("#products").html(content);
        }
    })
}

CallGetAll();


var selectedProduct;

function SelectProduct(id) {
    $("#productId").val(id);
    selectedProduct = products.find(p => p.id == id);
    console.log(selectedProduct);
}

function GetBarcode() {
    const volume = $("#volumeId").val();
    const obj = {
        "productId": selectedProduct.id,
        "volume": volume,
        "price": selectedProduct.price,
        "productName": selectedProduct.name
    }

    console.log(obj);

    $.ajax({
        url: "https://localhost:22955/b",
        method: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#result").html(response.data);
        },
        error: function (err) {
            console.log(err);
        }
    })

}

let element = document.getElementById('p-info');
function Search() {
    const value = document.getElementById("searchInput").value;
    if (String(value).trim() == '') {
        alert('please use Barcode Scanner');
    }
    else {
        $.ajax({
            url: `https://localhost:22955/s/${value}`,
            method: "GET",
            success: function (data) {
                let content = `
                    <section>
                    <img src='${data.imageUrl}' style='width:100px;height:100px' />
                    <h1>Name : ${data.productName}</h1>
                    <section>
                    <h5>Code : ${data.code}</h5>
                    <h3><b>Total Price : ${data.totalPrice}$</b></h3>
                    </section>
                    </section>
                `;
                element.innerHTML += content;
            }
        })
    }
}