import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { StatusTweet } from '../models/statusTweet';
import { OrgData } from 'angular-org-chart/src/app/modules/org-chart/orgData';

@Component({
  selector: 'app-thread-messages',
  templateUrl: './thread-messages.component.html',
  styleUrls: ['./thread-messages.component.scss']
})
export class ThreadMessagesComponent implements OnInit {
  public link: string;
  public allThread: StatusTweet;
  public jsonArr: StatusTweet;
  //public orgData: OrgData = {};

  orgData: OrgData = {
    isFavourite: true,
    retweetCount: 10,
    tweetText: 'fgjhghjfffff',
    language: 'fgj',
    mediaUrl: null,
    isPossiblySensitive: false,
    url: 'string',
    favoriteCount: 10,
    created: null,
    userName: 'string',
    userScreenName: 'string',
    replies: [
      {
        isFavourite: true,
        retweetCount: 10,
        tweetText: 'fgjhgh',
        language: 'fgj',
        mediaUrl: null,
        isPossiblySensitive: false,
        url: 'string',
        favoriteCount: 10,
        created: null,
        userName: 'string',
        userScreenName: 'string',
        replies: []
      }
    ],
  };

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  onGetAllTweets(link: string): void{
    this.tweetsService.getFullThread(link).subscribe((res: StatusTweet) => {
      this.allThread = null;
      this.allThread = res;
      this.orgData = this.allThread;
      this.jsonArr = JSON.parse(JSON.stringify(this.allThread));    
    }, error => {
      this.toastr.error(error, 'Error ocurred', { positionClass: 'toast-bottom-right' });
    }
    );
  }

  onClear(): void {
    this.link = '';
    this.allThread = null;
  }
}
