import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThoughtHoleWallComponent } from './thought-hole-wall/thought-hole-wall.component';
import { RouterModule } from '@angular/router';
import { ThoughtHoleRoutes } from './thoght-hole.routes';
import { SharedModule } from '../common/shared/shared.module';
import { MatGridListModule } from '@angular/material';



@NgModule({
  declarations: [
    ThoughtHoleWallComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ThoughtHoleRoutes),
    SharedModule
  ]
})
export class ThoughtHoleModule { }
