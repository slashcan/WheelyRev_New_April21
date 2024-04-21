const plus = document.querySelector(".plus");
const minus = document.querySelector(".minus");
const num = document.querySelector(".num");
const quantityHidden = document.getElementById("quantityHidden");
const availableQuantitySpan = document.getElementById("availableQuantity");

const productPrice = document.getElementById("productPrice");

let quantity = 1; // Initialize the quantity
var availableQuantity = parseInt(availableQuantitySpan.innerText); // Get the available quantity

plus.addEventListener("click", () => {
    if (quantity < availableQuantity)
    {
        quantity++;
        updateQuantity();
        total();
    }
    else {
        alert("The availalbe quantity is " + availableQuantity + ".");
    }
});

minus.addEventListener("click", () => {
    if (quantity > 1) {
        quantity--;
        updateQuantity();
        total();
    }
});

function updateQuantity() {
    num.textContent = quantity;
    quantityHidden.value = quantity;
}

function total() {
    let productPrice = parseFloat(document.getElementById("productPrice").innerHTML.replace("₱", ""));
    let quantity = parseInt(document.getElementById("quantityInput").innerText);
    let total = productPrice * quantity;
    document.getElementById("totalDisplay").innerText = "₱" + total.toFixed(2);
}