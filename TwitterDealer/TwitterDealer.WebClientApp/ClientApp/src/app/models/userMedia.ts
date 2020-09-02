import { StatusTweet } from '../models/statusTweet';
import { BaseUserMedia } from './baseUserMedia';

export interface UserMedia extends BaseUserMedia {
    tweetUrl: string;
    tweetContent: StatusTweet;
}

