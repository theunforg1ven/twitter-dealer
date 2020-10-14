import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AuthService } from './services/auth.service';
import { TweetsService } from './services/tweets.service';
import { UserService } from './services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './services/error.interceptor';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ThreadMessagesComponent } from './thread-messages/thread-messages.component';
import { UserThreadComponent } from './user-thread/user-thread.component';
import { UserTweetsComponent } from './user-tweets/user-tweets.component';
import { UserMediaComponent } from './user-media/user-media.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { AuthGuard } from './guards/auth.guard';
import { MembersComponent } from './members/members.component';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { MemberListResolver } from './resolvers/member-list.resolver';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { OrgChartModule } from 'angular-org-chart';
import { ChartsModule } from 'ng2-charts';
import { BarChartComponent } from './charts/bar-chart/bar-chart.component';
import { LineChartComponent } from './charts/line-chart/line-chart.component';
import { PieChartComponent } from './charts/pie-chart/pie-chart.component';

export function tokenGetter(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ThreadMessagesComponent,
    UserThreadComponent,
    UserTweetsComponent,
    UserMediaComponent,
    UserInfoComponent,
    MembersComponent,
    MemberListComponent,
    MemberDetailComponent,
    BarChartComponent,
    LineChartComponent,
    PieChartComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    NgxGalleryModule,
    HttpClientModule,
    OrgChartModule,
    ChartsModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:44320'],
        disallowedRoutes: ['localhost:44320/api/auth']
      }
    }),
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AuthGuard,
    UserService,
    MemberDetailResolver,
    MemberListResolver,
    TweetsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
