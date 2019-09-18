import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "@src/app/app-routing.module";
import { AppComponent } from "@src/app/app.component";
import { HttpClientModule } from "@angular/common/http";
import { HeadComponent } from "@src/app/head/head.component";
import { LeftPanelComponent } from "@src/app/left-panel/left-panel.component";
import { FooterComponent } from "@src/app/footer/footer.component";
import { LoginComponent } from "@src/app/login/login.component";
import { ThoughtCardComponent } from "@src/app/thought-card/thought-card.component";
import { SharedModule } from "@src/app/shared/shared.module";
import { ThoughtHoleWallComponent } from "@src/app/thought-hole-wall/thought-hole-wall.component";

@NgModule({
  declarations: [
    AppComponent,
    HeadComponent,
    LeftPanelComponent,
    FooterComponent,
    LoginComponent,
    ThoughtCardComponent,
    ThoughtHoleWallComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,

    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
