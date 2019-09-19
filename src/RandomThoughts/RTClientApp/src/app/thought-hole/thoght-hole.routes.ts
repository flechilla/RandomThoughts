import { ThoughtHoleWallComponent } from './thought-hole-wall/thought-hole-wall.component';
import { Routes } from '@angular/router';

export const ThoughtHoleRoutes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: ThoughtHoleWallComponent
    }//add the case for the user specific wall
];