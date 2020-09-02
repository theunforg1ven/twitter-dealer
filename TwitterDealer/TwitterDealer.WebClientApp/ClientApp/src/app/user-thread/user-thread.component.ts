import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { OrgData } from 'angular-org-chart/src/app/modules/org-chart/orgData';
import { StatusTweet } from '../models/statusTweet';

@Component({
  selector: 'app-user-thread',
  templateUrl: './user-thread.component.html',
  styleUrls: ['./user-thread.component.scss']
})
export class UserThreadComponent implements OnInit {
  public link: string;
  public allThread: StatusTweet;
  public showReplies: boolean;
  public orgData: OrgData;

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.showReplies = false;
  }

  onGetAllTweets(link: string): void{
    this.tweetsService.getUserThread(link).subscribe((res: StatusTweet) => {
      this.allThread = null;
      this.allThread = res;
      this.orgData = this.allThread;
    }, error => {
      this.toastr.error(error, 'Error ocurred', { positionClass: 'toast-bottom-right' });
      }
    );
  }

  onClear(): void {
    this.link = '';
    this.allThread = null;
    this.orgData = null;
  }

  onShowReplies(): void {
    this.showReplies = !this.showReplies;
  }

}
