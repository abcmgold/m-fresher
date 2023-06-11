import {createRouter, createWebHistory} from 'vue-router';
import OverView from '../views/overview/OverView'
import PropertyList from '../views/property/PropertyList'

// định nghĩa các router
const routers = [
    {path: '/', component: OverView, name:"OverViewRouter"},
    {path: '/property', component: PropertyList, name:"PropertyListRouter"}
]

// Khởi tạo router

const vueRouter = createRouter(
    {
        history: createWebHistory(), 
        routes: routers
    }
)

export default vueRouter