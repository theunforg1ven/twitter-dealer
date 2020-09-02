import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { StatusTweet } from '../models/statusTweet';

@Component({
  selector: 'app-user-tweets',
  templateUrl: './user-tweets.component.html',
  styleUrls: ['./user-tweets.component.scss']
})
export class UserTweetsComponent implements OnInit {
  public link: string;
  public allThread: StatusTweet[];

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  onGetUserTweets(link: string): void {
    this.tweetsService.getUserTweets(link).subscribe((res: StatusTweet[]) => {
      this.allThread = null;
      this.allThread = res;
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
