import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ThreadMessagesComponent } from './thread-messages/thread-messages.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { UserMediaComponent } from './user-media/user-media.component';
import { UserTweetsComponent } from './user-tweets/user-tweets.component';
import { UserThreadComponent } from './user-thread/user-thread.component';
import { AuthGuard } from './guards/auth.guard';


const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'threadmessages', component: ThreadMessagesComponent },
      { path: 'userthread', component: UserThreadComponent },
      { path: 'userinfo', component: UserInfoComponent },
      { path: 'usermedia', component: UserMediaComponent },
      { path: 'usertweets', component: UserTweetsComponent },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
