import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ThreadMessagesComponent } from './thread-messages/thread-messages.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { UserMediaComponent } from './user-media/user-media.component';
import { UserTweetsComponent } from './user-tweets/user-tweets.component';
import { UserThreadComponent } from './user-thread/user-thread.component';
import { AuthGuard } from './guards/auth.guard';
import { MembersComponent } from './members/members.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { MemberListResolver } from './resolvers/member-list.resolver';


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
      { path: 'members', component: MembersComponent, resolve: {users: MemberListResolver } },
      { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver} },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
