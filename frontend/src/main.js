import {createApp} from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import VueTippy from "vue-tippy";
import 'tippy.js/dist/tippy.css' // optional for styling

import VTooltip from 'v-tooltip'

/* import the fontawesome core */
import {library} from '@fortawesome/fontawesome-svg-core'

/* import font awesome icon component */
import {FontAwesomeIcon} from '@fortawesome/vue-fontawesome'


import MISAButton from './components/MISAButton.vue'
import MISATextInput from './components/MISATextInput.vue'
import MISAIcon from './components/MISAIcon.vue'
import MISAToast from './components/MISAToast.vue'
import MISAModal from './components/MISAModal.vue'
import MISACombobox from './components/MISACombobox.vue'
import MISANumberInput from './components/MISANumberInput.vue'
import MISAGroupInput from './components/MISAGroupInput.vue'
import MISADialog from './components/MISADialog.vue'
import MISACheckbox from './components/MISACheckbox.vue'
import MISADatePicker from './components/MISADatepicker.vue'
import MISAPagination from './components/MISAPagination.vue'
import MISAMoneyInput from './components/MISAMoneyInput.vue'
import MISAValidateText from './components/MISAValidateText.vue'
import MISAContextMenu from './components/MISAContextMenu.vue'
import MISASetting from './components/MISASetting.vue'

import {
    pressEscEvent,
    leftClickMouse,
    clickOutside,
    scrollOutside,
    resizeColumnDirective,
    resizeRowDirective
} from './common/directive.js'

import ENUM from './common/enum';

import {MISAResource} from './common/resource';

import vueRouter from './router';

import store from './store'


/* import specific icons */
import {faUserSecret, faSpinner, faCircleCheck} from '@fortawesome/free-solid-svg-icons'
/* add icons to the library */
library.add(faUserSecret, faSpinner, faCircleCheck)


const app = createApp(App);
app.component('m-button', MISAButton)
app.component('m-text-input', MISATextInput)
app.component('m-icon', MISAIcon)
app.component('m-toast', MISAToast)
app.component('m-modal', MISAModal)
app.component('m-combo-box', MISACombobox)
app.component('m-number-input', MISANumberInput)
app.component('m-dialog', MISADialog)
app.component('m-checkbox', MISACheckbox)
app.component('m-date-picker', MISADatePicker)
app.component('m-pagination', MISAPagination)
app.component('m-validate-text', MISAValidateText)
app.component('m-context-menu', MISAContextMenu)
/* add font awesome icon component */
app.component('font-awesome-icon', FontAwesomeIcon)
app.component('m-group-input', MISAGroupInput)
app.component('m-money-input', MISAMoneyInput)
app.component('m-setting', MISASetting)
app.component('m-tooltip', VTooltip)


// add directive
app.directive('esc', pressEscEvent);
app.directive('leftclick', leftClickMouse);
app.directive('clickoutside', clickOutside);
app.directive('scrolloutside', scrollOutside);
app.directive('resizecol', resizeColumnDirective)
app.directive('resizerow', resizeRowDirective)

// add store
app.use(store)

app.use(VueTippy,
// optional
    {
    directive: 'tippy', // => v-tippy
    component: 'tippy', // => <tippy/>
})

app.use(VTooltip)


app.config.globalProperties.$_MISAResource = MISAResource;
app.config.globalProperties.$_MISAEnum = ENUM;

app.use(ElementPlus)
app.use(vueRouter)

localStorage.setItem('language', 'vn-VI');

app.mount('#app')
