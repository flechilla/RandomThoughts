import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { NgModule } from "@angular/core";
import { MatInputModule, MatFormFieldModule, MatSidenavModule } from '@angular/material';
import { AppRoutingModule } from '../app-routing.module.tns';

@NgModule({
  imports: [
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatFormFieldModule,
    MatSidenavModule,
    AppRoutingModule,
  ],
  exports: [
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatFormFieldModule,
    MatSidenavModule,
    AppRoutingModule,
  ]
})
export class SharedModule {}
