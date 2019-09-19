import { Routes } from '@angular/router';

export const AppRoutes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'my-thoughts'
    },
    {
        path: '**',
        redirectTo: 'my-thoughts'
    },
    {
        path: 'my-thoughts',
        loadChildren: './thought-hole/thought-hole.module#ThoughtHoleModule'
    }
];