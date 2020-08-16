import { BaseUserMedia } from './baseUserMedia';

export interface StatusTweet {
    isFavourite: boolean;
    retweetCount: number;
    tweetText: string;
    language: string;
    mediaUrl: BaseUserMedia[];
    isPossiblySensitive?: boolean;
    url: string;
    favoriteCount: number;
    created?: Date;
    userName: string;
    userScreenName: string;
    replies: StatusTweet[];
}
