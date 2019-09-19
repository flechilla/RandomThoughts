import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThoughtCardComponent } from './thought-card/thought-card.component';
import { MatCardModule } from '@angular/material';
import { SharedModule } from '../common/shared/shared.module';



@NgModule({
  declarations: [
    ThoughtCardComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ThoughtModule { }
