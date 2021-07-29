import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/participantprofile/:nickname',
    name: 'ParticipantProfile',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import('../views/ParticipantProfile.vue')
  },
  {
    path: '/organizerprofile',
    name: 'OrganizerProfile',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import('../views/OrganizerProfile.vue')
  },
  {
    path: '/project/:id',
    name: 'Project',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import('../views/Project.vue')
  },
  {
    path: '/project/:proj_id/read/:id',
    name: 'Read',
    component: () => import('../views/NewsRead.vue')
  },
  {
    path: '/users',
    name: 'Users',
    component: () => import('../views/Users.vue')
  },
  {
    path: '/event/:id',
    name: 'Event',
    component: () => import('../views/Event.vue')
  },
  {
    path: '/signin',
    name: 'SignIn',
    component: () => import('../views/SignIn.vue')
  },
  {
    path: '/projectspage',
    name: 'ProjectsPage',
    component: () => import('../views/ProjectsPage.vue')
  },
  {
    path: '/eventspage',
    name: 'EventsPage',
    component: () => import('../views/EventsPage.vue')
  },
  {
    path: '/createproject',
    name: 'CreateProject',
    component: () => import('../views/CreateProject.vue')
  },
  {
    path: '/createevent',
    name: 'CreateEvent',
    component: () => import('../views/CreateEvent.vue')
  },
  {
    path: '/:pathMatch(.*)*',
    name: '404',
    component: () => import('../views/404.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
