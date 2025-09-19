import api from "../api/apiClient.js";
import {setTokens, clearTokens} from '../api/authService.js';

const msg = sessionStorage.getItem('alertMessage');
if (window.location.href in ['', '/'] && msg){
    const alert = document.getElementById('alert');
    let alertClassName;
    if (msg in ['Добро пожаловать', 'Регистрация прошла успешно']){
        alertClassName = 'alert-success'
    } else {
        alertClassName = 'alert-danger'
    }
    alert.classList.add(alertClassName);
    alert.textContent = msg;
    alert.classList.remove('d-none');
    sessionStorage.removeItem('alertMessage');
    setTimeout(() => alert.classList.add('d-none'), 3000);
}

function getDataFromForm(form){
    const formData = new FormData(form);
    const payload = Object.fromEntries(formData.entries());
    return JSON.stringify(payload);
}

const logoutForm = document.getElementById('logoutForm')

if (logoutForm) {
    logoutForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const url = logoutForm.getAttribute('action');
        const json = getDataFromForm(logoutForm);
        const response = await api.post(url, json);
        if (response.Ok) {
            clearTokens();
            window.location.href = '/';
        }
    })
}

const loginForm = document.getElementById('loginForm')
if (loginForm) {
    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const url = loginForm.getAttribute('action');
        const json = getDataFromForm(loginForm);
        const response = await api.post(url, json);
        try {
            if (response.status === 200 || response.status === 201) {
                sessionStorage.setItem('alertMessage', 'Добро пожаловать')
                setTokens(response.data);
                window.location.href = '/';
            } else {
                const alert = document.getElementById('alert');
                alert.classList.add('alert-danger');
                alert.classList.remove('d-none');
                alert.textContent = 'Неудачная попытка авторизации';
                setTimeout(() => {
                    alert.classList.remove('alert-danger');
                    alert.classList.add('d-none');
                }, 3000);
            }
        } catch (error) {
            const alert = document.getElementById('alert');
            alert.classList.add('alert-danger');
            alert.classList.remove('d-none');
            alert.textContent = 'Ошибка авторизации';
            setTimeout(() => {
                alert.classList.remove('alert-danger');
                alert.classList.add('d-none');
            }, 3000);
        }
        
    })
}

const registerForm = document.getElementById('registerForm')
if (registerForm) {
    registerForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const url = registerForm.getAttribute('action');
        const formData = new FormData(registerForm);
        const payload = Object.fromEntries(formData.entries());
        const json = JSON.stringify(payload);
        const response = await api.post(url, json);
        if (response.Ok) {
            sessionStorage.setItem('alertMessage', 'Регистрация прошла успешно')
            window.location.href = '/';
        }
    })
}