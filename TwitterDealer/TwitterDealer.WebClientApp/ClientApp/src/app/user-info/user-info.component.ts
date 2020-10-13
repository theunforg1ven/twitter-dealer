import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TweetsService } from '../services/tweets.service';
import { MainUserModel } from '../models/mainUserModel';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  public link: string;
  public userData: MainUserModel;
  public profileBackgroundColor: string;
  public profileTextColor: string;
  public profileLinkColor: string;

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  onGetUserData(link: string): void {
    if (link == null) {
      return;
    }

    this.tweetsService.getUserInfo(link).subscribe((res: MainUserModel) => {
      this.userData = null;
      this.userData = res;
      this.changeImageSize();
      this.changeLanguage();
      this.addColorVariable();
    }, error => {
      this.toastr.error(error, 'Error ocurred', { positionClass: 'toast-bottom-right' });
      }
    );
  }

  onClear(): void {
    this.link = '';
    this.userData = null;
  }

  private changeImageSize(): void {
    if (this.userData !== null &&  this.userData !== undefined) {
      this.userData.imageUrl = this.userData.imageUrl.replace('normal', '400x400');
    }
  }

  private changeLanguage(): void {
    if (this.userData !== null &&  this.userData !== undefined) {
      this.userData.language = 'no available info :C';
    }
  }

  private addColorVariable(): void {
    if (this.userData !== null &&  this.userData !== undefined) {
      this.profileBackgroundColor = '#' + this.userData.profileBackgroundColor;
      this.profileTextColor = '#' + this.userData.profileTextColor;
      this.profileLinkColor = '#' + this.userData.profileLinkColor;
    }
  }
}
