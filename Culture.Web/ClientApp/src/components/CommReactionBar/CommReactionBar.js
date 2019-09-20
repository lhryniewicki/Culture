
import React from 'react';
import Comment from '../Comment/Comment';
import Reactions from '../Reactions/Reactions';
import { sendComment, getMoreComments } from '../../api/CommentApi';


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
            reactions: {
                angry: 0,
                haha: 0,
                like: 0,
                love: 0,
                sad: 0,
                wow: 0
            },
            commentContent: '',
            reactionsCount: this.props.reactionsCount,
            commentsCount: this.props.commentsCount,
            showReactionModal: false,
            showComments: false,
            commentsPage: 1,
            mouseCoords: 0,
            canLoadMoreComments:this.props.comments.loadMore
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

        let reactionState = { ...this.state.reactions };
        reactionState[e.target.name] += 1;

        await this.setState({
            reactions: reactionState,
            reactionsCount: this.state.reactionsCount + 1
        });
        this.sortReactions();


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
            let jsDateFormatted = `${jsDate.getDay()}-${jsDate.getMonth()}-${jsDate.getFullYear()} ${jsDate.getHours()}:${jsDate.getMinutes()}`;
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
       const newComments = await getMoreComments(this.props.id, this.state.commentsPage );
       console.log(newComments);
        const items = [...this.state.comments, newComments.commentsList];
        //CALL API SERWER
        this.setState({
            commentsPage: this.state.commentsPage + 1,
            comments: items
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
        //this.sortReactions();
        var items = [];
        Object.entries(this.state.reactions).forEach(([key, reaction], index) => {
            if (reaction > 0)
                items.push(<img draggable={false}
                    onMouseOver={this.onReactionMouseOver}
                    name={key}
                    src={images[key]}
                    width="25px"
                    height="25px"
                    data-toggle="popover"
                    key={index}
                    data-placement="top"
                    title={`Ilość reakcji: ` + reaction} />)
        });
        return items;
    }
    handleCommentSubmit = async (e) => {
        e.preventDefault();
         const newComment = await sendComment(this.props.id, this.state.commentContent, 'b5ce53d5-978f-42bf-74da-08d73cef40dc');

        this.setState(prevState => ({
            commentsCount: this.state.commentsCount+1,
            comments: [newComment, ...prevState.comments ]
         }))
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
                            <a href="" onClick={this.onReactionClick} className="comment" style={{ marginRight: "50px" }}> Reaguj</a>

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
                        <div className="text-center">
                            <a href="#" onClick={this.moreComments}>Załaduj więcej</a>
                        </div>
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



