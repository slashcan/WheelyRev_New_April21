function showPreview(imagePath, productName, productDescription, productPrice, productID, shopId) {

    productPrice = parseFloat(productPrice).toFixed(2);

    var previewHtml = `
            <div class="preview">
                <img src="Images/${imagePath}" alt="${productName}">
                <h3>${productName}</h3>
                <p>${productDescription}</p>
                <div class="price">${productPrice}</div>
                <div class="buttons">
                    <a href="/BuyNow/BuyNow?productId=${productID}&shopId=${shopId}" class="buy">Buy now</a>
                    <a href="#" class="cart">Add to cart</a>
                </div>
            </div>
        `;
    document.getElementById("products-preview").innerHTML = previewHtml;
    document.getElementById("preview-container").style.display = "flex";
    document.body.style.overflow = "hidden"; // Disable scrolling when in preview mode
}

function closePreview() {
    document.getElementById("preview-container").style.display = "none";
    document.body.style.overflow = "auto"; // Enable scrolling when exiting preview mode
}