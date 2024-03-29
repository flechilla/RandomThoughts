import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRoutes } from './app.routes';


@NgModule({
  imports: [RouterModule.forRoot(AppRoutes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
