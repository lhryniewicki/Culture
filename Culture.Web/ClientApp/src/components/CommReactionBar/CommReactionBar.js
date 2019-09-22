
import React from 'react';
import Comment from '../Comment/Comment';
import Reactions from '../Reactions/Reactions';
import { sendComment, getMoreComments } from '../../api/CommentApi';
import { sendReaction } from '../../api/EventApi';


const images = {
    like: require('../../assets/reactions/like.svg'),
    love: require('../../assets/reactions/love.svg'),
    wow: require('../../assets/reactions/wow.svg'),
    haha: require('../../assets/reactions/haha.svg'),
    angry: require('../../assets/reactions/angry.svg'),
    sad: require('../../assets/reactions/sad.svg')
};
class CommReactionBar extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            comments: this.props.comments,
            reactions: this.props.reactions,
            commentContent: '',
            reactionsCount: this.props.reactionsCount,
            commentsCount: this.props.commentsCount,
            showReactionModal: false,
            showComments: false,
            commentsPage: 1,
            mouseCoords: 0,
            currentReaction: this.props.currentReaction,
            canLoadMoreComments: this.props.canLoadMore
        }



        this.showComments = this.showComments.bind(this);
        this.moreComments = this.moreComments.bind(this);
        this.onReactionClick = this.onReactionClick.bind(this);
        this.onReactionSend = this.onReactionSend.bind(this);
        this.closeModal = this.closeModal.bind(this);
        this.sortReactions = this.sortReactions.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.displayComments = this.displayComments.bind(this);
    }
    handleInputChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    closeModal() {
        this.setState({ showReactionModal: false });
    }
    onReactionClick(e) {
        e.preventDefault();
        this.setState({
            mouseCoords: e.clientY
            ,
            showReactionModal: true
        });
    }
    async onReactionSend(e) {
        const name = e.target.name;
        const result = await sendReaction('b5676a5b-0c37-46f5-3147-08d73e6da2eb', this.props.id, name);
        if (name === this.state.currentReaction) {
            this.setState({
                currentReaction: null,
                reactions: result.reactions,
                reactionsCount: this.state.reactionsCount - 1
            });
        }
        else if (this.state.currentReaction === null) {
            this.setState({
                currentReaction: name,
                reactions: result.reactions,
                reactionsCount: this.state.reactionsCount + 1
            });
        }
        else {
            this.setState({
                currentReaction: name,
                reactions: result.reactions
            });
        }
        this.closeModal();
    }
    showComments(event) {
        event.preventDefault();
        this.setState({
            showComments: !this.state.showComments
        });

    }
    displayComments() {
        let items = [];
        this.state.comments.map((c, index) => {
            let jsDate = new Date(Date.parse(c.creationDate));
            let jsDateFormatted = `${jsDate.getDay()}-${jsDate.getMonth()}-${jsDate.getFullYear()} ${jsDate.getHours()}:${(jsDate.getMinutes() < 10 ? '0' : '') + jsDate.getMinutes()}`;
                items.push(
                    <Comment
                        key={index}
                        content={c.content}
                        author={c.authorName}
                        creationDate={jsDateFormatted}
                    />)
        })

        return items;
    }
   async moreComments(event) {
        event.preventDefault();
       const newComments = await getMoreComments(this.props.id, this.state.commentsPage);
       const items = this.state.comments.concat(newComments.commentsList);
        //CALL API SERWER
        this.setState({
            commentsPage: this.state.commentsPage + 1,
            comments: items,
            canLoadMoreComments: newComments.canLoadMore
        });
    }
    sortReactions() {
        var sorted = [];
        for (var reaction in this.state.reactions) {
            sorted.push([reaction, this.state.reactions[reaction]]);
        }
        sorted.sort((a, b) => {
            return b[1] - a[1];
        });
        var result = {};
        sorted.forEach((item) => {
            result[item[0]] = item[1];
        });
        this.setState({ reactions: result });
    }
    displaySortedReactions() {
        console.log(this.state.reactions)
        //this.sortReactions();
        let items = [];
        if (this.state.reactions.length > 0) {
            this.state.reactions.map((element, index) => {
                console.log(element)
                items.push(<img draggable={false}
                    name={element.reactionType}
                    src={images[element.reactionType]}
                    width="25px"
                    height="25px"
                    data-toggle="popover"
                    key={index}
                    data-placement="top"
                    title={`Ilość reakcji: ` + element.count} />);
            });
            
        }
        return items;
    }
    handleCommentSubmit = async (e) => {
        e.preventDefault();
         const newComment = await sendComment(this.props.id, this.state.commentContent, 'b5ce53d5-978f-42bf-74da-08d73cef40dc');

        this.setState(prevState => ({
            showComments:true,
            commentContent:'',
            commentsCount: this.state.commentsCount+1,
            comments: [newComment, ...prevState.comments ]
         }))
    }
    displayCurrentReaction = () => {
        if (this.state.currentReaction !== null)
            return <img
                    src={images[this.state.currentReaction]}
                    data-placement="top"
                    title={`Twoja reakcja`}
                    data-toggle="popover"
                    width="25px"
                    height="25px"
                    />
    }
    render() {
        return (
            <div>
                <div className="card-footer text-muted">
                    Umieszczono dnia {this.props.date} przez
            <a href="#"> {this.props.createdBy} </a>
                    {
                        this.displaySortedReactions()
                    }


                    {this.state.reactionsCount}
                    <div className="pull-right">
                        <div className="float-left">
                            <a href="" onClick={this.onReactionClick} className="comment" style={{ marginRight: "50px" }}> Reaguj {this.displayCurrentReaction()}</a>

                            <a href="" onClick={this.showComments} className="comment">  Komentarze: {this.state.commentsCount}</a>
                        </div>
                    </div>

                    <Reactions images={images}
                        onReactionSend={this.onReactionSend}
                        mouseCoords={this.state.mouseCoords}
                        closeModal={this.closeModal}
                        showModal={this.state.showReactionModal} />

                </div>
                {this.state.showComments === true
                    ?
                    <div>

                        {
                            this.displayComments()
                        }
                        {this.state.canLoadMoreComments === true
                            ?
                            <div className="text-center">
                                <a href="#" onClick={this.moreComments}>Załaduj więcej</a>
                            </div>
                            :
                            null
                        }
                    </div>

                    :
                    null
                }

                <div className="card-footer text-muted">
                    <form onSubmit={this.handleCommentSubmit}>
                        <div className="form-group">
                            <input
                                className="form-control input-sm comment"
                                placeholder="Wpisz komentarz..."
                                onChange={this.handleInputChange}
                                type="text"
                                value={this.state.commentContent}
                                name="commentContent" />
                        </div>
                    </form>
                </div>

            </div>

        );
    }
}
export default CommReactionBar;



