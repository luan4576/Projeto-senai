export const Autenticado = () => localStorage.getItem('token') !== null

export const parseJwt = () => {
    var token = localStorage.getItem('token')
    if(token){
        var base64Url = token.split('.')[1]
        var base64 = base64Url.replace(/-/g, '/')
        return JSON.parse(window.atob(base64))  
    }
}