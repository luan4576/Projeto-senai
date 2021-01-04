
$(document).load(function () {
    if(localStorage.getItem('token') !== undefined){
        const elemento = document.getElementById('#centro-direito-flex-btn')
        elemento.style.display = 'none'
    }
})

