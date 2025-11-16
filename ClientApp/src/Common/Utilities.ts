import type NavItem from './NavItem'

// Automatically import all page components and stores
const components = import.meta.glob('../Pages/*/index.vue', { eager: true })
const compStores = import.meta.glob('../Pages/*/Store.ts', { eager: true })

export let discoverAllPages = (navItems: Array<NavItem>) => {
    const allPages = Object.entries(components)
        .map((component, index) => {
            const elem = component as any
            const storeModule = Object.values(compStores)[index] as any
            const StoreClass = storeModule.default
            const store = navItems.find(x => x.constructor === StoreClass)

            return {
                component: elem[1].default,
                store: store as NavItem,
                name: elem[0].split('/')[2]
            }
        })
        .filter(page => page.store !== undefined) // Skip pages with no matching store in navItems

    return allPages
}

export function sleep(ms: number, id?: string) {
    return new Promise((resolve) => setTimeout(resolve, ms));
}