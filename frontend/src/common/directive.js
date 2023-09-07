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

const leftClickMouse = {
    mounted(el, binding) {
        el.leftClickHandler = event => {
            if (event.button === 0) { // Kiểm tra nút chuột là nút trái (0)
                binding.instance.$store.commit('hideContextMenu');
            }
        };
        el.addEventListener('click', el.leftClickHandler);
    },
    unmounted(el) {
        if (el.leftClickHandler) {
            el.removeEventListener('click', el.leftClickHandler);
            delete el.leftClickHandler;
        }
    }
};

const clickOutside = {
    mounted(el, binding) { // Lấy giá trị ràng buộc function từ directive
        const onClickOutside = binding.value;
        // Function để kiểm tra xem sự kiện click có diễn ra bên ngoài phần tử không
        el.onClickHandler = event => {
            if ((el !== event.target && !el.contains(event.target))) { // Gọi function truyền vào thông qua giá trị ràng buộc
                onClickOutside();
            }
        };
        // Thêm sự kiện click vào document.body để kiểm tra click bên ngoài phần tử
        document.addEventListener('click', el.onClickHandler);
    },
    unmounted(el) { // Xóa sự kiện khi directive unmounted
        if (el.onClickHandler) {
            document.removeEventListener('click', el.onClickHandler);
            delete el.onClickHandler;
        }
    }
};

const scrollOutside = {
    mounted(el, binding) { // Lấy giá trị ràng buộc function từ directive
        const onScrollOutside = binding.value;
        // Function để kiểm tra xem sự kiện click có diễn ra bên ngoài phần tử không
        el.onScrollHandler = event => {
            if ((!el.contains(event.target))) { // Gọi function truyền vào thông qua giá trị ràng buộc
                onScrollOutside();
            }
        };

        // Thêm sự kiện click vào document.body để kiểm tra click bên ngoài phần tử
        document.addEventListener('scroll', el.onScrollHandler);
    },
    unmounted(el) { // Xóa sự kiện khi directive unmounted
        if (el.onScrollHandler) {
            document.removeEventListener('scroll', el.onScrollHandler);
            delete el.onScrollHandler;
        }
    }
};

export {
    clickOutside,
    pressEscEvent,
    leftClickMouse,
    scrollOutside
};
