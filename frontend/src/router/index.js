import {createRouter, createWebHistory} from 'vue-router';
import OverView from '../views/overview/OverView'
import PropertyList from '../views/property/PropertyList'
import PropertyTransfer from '../views/property/PropertyTransfer'

// định nghĩa các router
const routers = [
    {path: '/', redirect: '/property',component: OverView, name:"OverViewRouter"},
    {path: '/property', component: PropertyList, name:"PropertyListRouter"},
    {path: '/property-transfer', component: PropertyTransfer, name:"PropertyTranferRouter"}
]

// Khởi tạo router

const vueRouter = createRouter( 
    {
        history: createWebHistory(), 
        routes: routers
    }
)

export default vueRouter