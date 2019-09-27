import React from 'react';
import { Modal } from 'react-bootstrap';
class Reactions extends React.Component {

    constructor(props) {
        super(props);

        this.renderImages = this.renderImages.bind(this);
    }
    renderImages() {
        let images = [];

        Object.keys(this.props.images).forEach((element, index) => {
            images.push(<img draggable={false} name={element} onClick={this.props.onReactionSend} src={this.props.images[element]} width="50px" height="50px" key={index} />)
        });
        return images;
    }
    render() {
        return (
            <Modal style={{ top: this.props.mouseCoords-150}} show={this.props.showModal} onHide={this.props.closeModal} >
                <Modal.Body>
                    {this.renderImages()}
                </Modal.Body>
                <Modal.Footer>
                    <button className="btn btn-secondary" onClick={this.props.closeModal}>
                        Zamknij
                             </button>
                </Modal.Footer>

            </Modal>
        );
    }
}
export default Reactions;



