// Sepet dizisi
let cart = [];

// Sepete kurs ekleme işlevi
document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', function() {
        let course = this.getAttribute('data-course');
        let price = this.getAttribute('data-price');
        
        // Sepete kursu ekle
        cart.push({ course, price });

        // Sepeti güncelle
        updateCart();
    });
});

// Sepet içeriğini güncelleme işlevi
function updateCart() {
    const cartItems = document.getElementById('cartItems');
    cartItems.innerHTML = '';

    // Sepet boşsa gösterme
    if (cart.length === 0) {
        cartItems.innerHTML = '<li class="list-group-item">Sepetiniz boş.</li>';
        return;
    }

    // Sepetteki tüm kursları listele
    cart.forEach(item => {
        const li = document.createElement('li');
        li.classList.add('list-group-item');
        li.textContent = `${item.course} - ₺${item.price}`;
        cartItems.appendChild(li);
    });

    // Sepet butonunu güncelle
    document.getElementById('viewCartBtn').textContent = `Sepetim (${cart.length})`;
}