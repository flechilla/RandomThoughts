import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "@src/app/app-routing.module";
import { AppComponent } from "@src/app/app.component";
import { AutoGeneratedComponent } from "@src/app/auto-generated/auto-generated.component";
import { HttpClientModule } from "@angular/common/http";
import { HeadComponent } from '@src/app/head/head.component';
import { LeftPanelComponent } from '@src/app/left-panel/left-panel.component';
import { FooterComponent } from '@src/app/footer/footer.component';
import { LoginComponent } from '@src/app/login/login.component';

@NgModule({
  declarations: [AppComponent, AutoGeneratedComponent, HeadComponent, LeftPanelComponent, FooterComponent, LoginComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
