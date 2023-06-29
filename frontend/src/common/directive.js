const pressEscEvent = {
    beforeMount(el, binding) {
        el.pressEsc = event => {
            if (event.key === 'Escape') {
                binding.value()
            }
        }
        document.addEventListener('keydown', el.pressEsc)
    },
    unmounted(el) {
        if (el.pressEsc) {
            document.removeEventListener('keydown', el.pressEsc);
            delete el.pressEsc;
        }
    }
}

export default pressEscEvent;
