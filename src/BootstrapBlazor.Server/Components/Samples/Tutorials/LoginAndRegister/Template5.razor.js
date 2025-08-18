export function init(id) {
    const el = document.getElementById(id);
    if (el) {
        const email = el.querySelector('.login-item-email');
        if (email) {
            email.classList.add('show');
            const input = email.querySelector('.input');
            if (input) {
                input.focus();
                input.select();
            }
        }
    }
}

export function go(id) {
    const el = document.getElementById(id);
    if (el) {
        const email = el.querySelector('.login-item-email');
        if (email) {
            email.classList.remove('show');
            email.classList.remove('animate-fade-in')
            email.classList.add('animate-fade-out');
        }

        const password = el.querySelector('.login-item-password');
        if (password) {
            password.classList.add('show');
            password.classList.add('animate-fade-in')
            password.classList.remove('animate-fade-out')
            const input = password.querySelector('.input');
            if (input) {
                input.focus();
                input.select();
            }
        }
    }
}

export function back(id) {
    const el = document.getElementById(id);
    if (el) {
        const email = el.querySelector('.login-item-email');
        if (email) {
            email.classList.add('show');
            email.classList.remove('animate-fade-out');
            email.classList.add('animate-fade-in');
            const input = email.querySelector('.input');
            if (input) {
                input.focus();
                input.select();
            }
        }

        const password = el.querySelector('.login-item-password');
        if (password) {
            password.classList.remove('show');
            password.classList.remove('animate-fade-in')
            password.classList.add('animate-fade-out')
        }
    }
}
