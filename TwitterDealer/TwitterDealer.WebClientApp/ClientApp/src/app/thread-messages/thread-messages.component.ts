import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { StatusTweet } from '../models/statusTweet';

@Component({
  selector: 'app-thread-messages',
  templateUrl: './thread-messages.component.html',
  styleUrls: ['./thread-messages.component.scss']
})
export class ThreadMessagesComponent implements OnInit {
  public link: string;
  public allThread: StatusTweet;

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  onGetAllTweets(link: string): void{
    this.tweetsService.getFullThread(link).subscribe((res: StatusTweet) => {
      this.allThread = null;
      this.allThread = res;
      console.log(res);
      //this.toastr.success('All is ok', 'Info', { positionClass: 'toast-bottom-right' });
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
